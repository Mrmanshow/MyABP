<template>
    <div>
        <Modal
                :title="L('EditNotifications')"
                :value="value"
                @on-ok="save"
                @on-visible-change="visibleChange"
                :scrollable="false"
        >
            <Form ref="notificationsForm" :model="notifications">
                <Tabs value="detail">
                    <TabPane :label="L('UserDetails')" name="detail">
                        <FormItem :label="L('Name')" prop="notificationName">
                            <Input readonly v-model="notifications.notificationName" :maxlength="96"></Input>
                        </FormItem>
                        <FormItem :label="L('DisplayName')" prop="displayName">
                            <Input v-model="notifications.displayName" :maxlength="50"></Input>
                        </FormItem>
                        <FormItem :label="L('Content')" prop="content">
                            <Input v-model="notifications.content" :maxlength="200"></Input>
                        </FormItem>
                        <FormItem :label="L('Status')" prop="status">
                            <RadioGroup v-model="notifications.status">
                                <Radio :label="0" true-value="0">
                                    <span>{{ L('Yes') }}</span>
                                </Radio>
                                <Radio :label="1" true-value="1">
                                    <span>{{ L('No') }}</span>
                                </Radio>
                            </RadioGroup>
                        </FormItem>
                    </TabPane>
                    <TabPane :label="L('NotificationsUserList')" name="name3">
                        <Form ref="queryForm" :label-width="80" label-position="left" inline>
                            <Row :gutter="16">
                                <Col span="16">
                                    <FormItem :label="L('Keyword')+':'" style="width:100%">
                                        <Input v-model="pagerequest.keyword" :placeholder="L('Notifications')+'/'+L('Name')"></Input>
                                    </FormItem>
                                </Col>
                            </Row>
                            <Row>
                                <Button icon="ios-search" type="primary" size="large" @click="getpage">{{L('Find')}}</Button>
                            </Row>
                        </Form>
                        <div class="margin-top-10">
                            <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                            </Table>
                            <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                        </div>
                        </TabPane>
                </Tabs>
            </Form>
            <div slot="footer">
                <Button @click="cancel">{{L('Cancel')}}</Button>
                <Button @click="save" type="primary">{{L('OK')}}</Button>
            </div>
        </Modal>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '../../../lib/util'
    import AbpBase from '../../../lib/abpbase'
    import Notifications from '../../../store/entities/notifications'
    import PageRequest from '@/store/entities/page-request'

    class  PageNotificationSubscriptionInfoRequest extends PageRequest{
        keyword:string;
        notificationName:string;
    }

    @Component
    export default class EditNotifications extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        notifications:Notifications=new Notifications();

        pagerequest:PageNotificationSubscriptionInfoRequest=new PageNotificationSubscriptionInfoRequest();

        get list(){
            return this.$store.state.notificationSubscriptionInfo.list;
        };
        get loading(){
            return this.$store.state.notificationSubscriptionInfo.loading;
        };
        get pageSize(){
            return this.$store.state.notificationSubscriptionInfo.pageSize;
        }
        get totalCount(){
            return this.$store.state.notificationSubscriptionInfo.totalCount;
        }
        get currentPage(){
            return this.$store.state.notificationSubscriptionInfo.currentPage;
        }

        get editNotifications(){
            return this.$store.state.notifications.editNotifications;
        }


        columns=[{
            title:this.L('UserName'),
            key:'userName'
        },{
            title:this.L('RoleName'),
            key:'roleNames'
        },{
            title:this.L('Actions'),
            key:'Actions',
            width:150,
            render:(h:any,params:any)=>{
                return h('div',[
                    h('Button',{
                        props:{
                            type:'error',
                            size:'small'
                        },
                        on:{
                            click:async ()=>{
                                this.$Modal.confirm({
                                    title:this.L('Tips'),
                                    content:this.L('UnsubscribeNotificationsConfirm'),
                                    okText:this.L('Yes'),
                                    cancelText:this.L('No'),
                                    onOk:async()=>{
                                        await this.$store.dispatch({
                                            type:'notificationSubscriptionInfo/unsubscribe',
                                            data:params.row
                                        })
                                        await this.getpage();
                                    }
                                })
                            }
                        }
                    },this.L('Unsubscribe'))
                ])
            }
        }]

        pageChange(page:number){
            this.$store.commit('notificationSubscriptionInfo/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('notificationSubscriptionInfo/setPageSize',pagesize);
            this.getpage();
        }
        async getpage(){
            this.pagerequest.maxResultCount=this.pageSize;
            this.pagerequest.skipCount=(this.currentPage-1)*this.pageSize;

            await this.$store.dispatch({
                type:'notificationSubscriptionInfo/getAll',
                data:this.pagerequest
            })
        }

        save(){
            (this.$refs.notificationsForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                        type:'notifications/update',
                        data:this.notifications
                    });
                    (this.$refs.notificationsForm as any).resetFields();
                    this.$emit('save-success');
                    this.$emit('input',false);
                }
            })
        }
        cancel(){
            (this.$refs.notificationsForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.cancel();
                this.$emit('input',value);
            }else{
                this.pagerequest.notificationName = this.editNotifications.notificationName;
                this.$store.dispatch('notifications/get',this.editNotifications.id).then(()=>{
                    this.notifications=Util.extend(true,{},this.$store.state.notifications.editNotifications);
                });
                this.getpage();
            }
        }
    }
</script>