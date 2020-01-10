<template>
    <div>
        <Card dis-hover>
            <div class="page-body">
                <Form ref="queryForm" :label-width="80" label-position="left" inline>
                    <Row :gutter="16">
                        <Col span="6">
                        <FormItem :label="L('Keyword')+':'" style="width:100%">
                            <Input v-model="pagerequest.keyword" :placeholder="L('Notifications')+'/'+L('Name')"></Input>
                        </FormItem>
                        </Col>
                    </Row>
                    <Row>
                        <Button @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
                        <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Find')}}</Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                    </Table>
                    <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                </div>
            </div>
        </Card>

        <create-notifications v-model="createModalShow"  @save-success="getpage"></create-notifications>
        <edit-notifications v-model="editModalShow"  @save-success="getpage"></edit-notifications>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'
    import CreateNotifications from './create-notifications.vue'
    import EditNotifications from './edit-notifications.vue'

    class  PageNotificationsRequest extends PageRequest{
        keyword:string;
        from:Date;
        to:Date;
    }

    @Component({
        components:{CreateNotifications,EditNotifications}
    })
    export default class Notifications extends AbpBase{
        //filters
        pagerequest:PageNotificationsRequest=new PageNotificationsRequest();
        creationTime:Date[]=[];
        createModalShow:boolean=false;
        editModalShow:boolean=false;

        get list(){
            return this.$store.state.notifications.list;
        };
        get loading(){
            return this.$store.state.notifications.loading;
        };
        get pageSize(){
            return this.$store.state.notifications.pageSize;
        }
        get totalCount(){
            return this.$store.state.notifications.totalCount;
        }

        edit(){
            this.editModalShow=true;
        }
        create(){
            this.createModalShow=true;
        }
        pageChange(page:number){
            this.$store.commit('notifications/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('notifications/setPageSize',pagesize);
            this.getpage();
        }
        async getpage(){
            this.pagerequest.maxResultCount=this.pageSize;
            this.pagerequest.skipCount=(this.currentPage-1)*this.pageSize;

            //filters
            if (this.creationTime.length>0) {
                this.pagerequest.from=this.creationTime[0];
            }
            if (this.creationTime.length>1) {
                this.pagerequest.to=this.creationTime[1];
            }

            await this.$store.dispatch({
                type:'notifications/getAll',
                data:this.pagerequest
            })
        }

        get currentPage(){
            return this.$store.state.notifications.currentPage;
        }
        columns=[{
            title:this.L('Name'),
            key:'notificationName'
        },{
            title:this.L('DisplayName'),
            key:'displayName'
        },{
            title:this.L('SubscribedNumber'),
            key:'subscribedNumber'
        },{
            title:this.L('PushNumber'),
            key:'pushNumber'
        },{
            title:this.L('Status'),
            render:(h:any,params:any)=>{
                return h('span',params.row.status?this.L('No'):this.L('Yes'))
            }
        },{
            title:this.L('Actions'),
            key:'Actions',
            width:150,
            render:(h:any,params:any)=>{
                return h('div',[
                    h('Button',{
                        props:{
                            type:'primary',
                            size:'small'
                        },
                        style:{
                            marginRight:'5px'
                        },
                        on:{
                            click:()=>{
                                this.$store.commit('notifications/edit',params.row);
                                this.edit();
                            }
                        }
                    },this.L('Edit')),
                    h('Button',{
                        props:{
                            type:'error',
                            size:'small'
                        },
                        on:{
                            click:async ()=>{
                                this.$Modal.confirm({
                                    title:this.L('Tips'),
                                    content:this.L('DeleteNotificationsConfirm'),
                                    okText:this.L('Yes'),
                                    cancelText:this.L('No'),
                                    onOk:async()=>{
                                        await this.$store.dispatch({
                                            type:'notifications/delete',
                                            data:params.row
                                        })
                                        await this.getpage();
                                    }
                                })
                            }
                        }
                    },this.L('Delete'))
                ])
            }
        }]
        async created(){
            this.getpage();
        }
    }
</script>