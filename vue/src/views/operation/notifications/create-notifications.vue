<template>
    <div>
        <Modal
         :title="L('CreateNewBanner')"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="notificationsForm"  label-position="top" :rules="notificationsRule" :model="notifications">
                <Tabs value="detail">
                    <TabPane :label="L('UserDetails')" name="detail">
                        <FormItem :label="L('Name')" prop="notificationName">
                            <Input v-model="notifications.notificationName" :maxlength="96"></Input>
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
    import AbpBase from '../../../lib/abpbase'
    import Notifications from '../../../store/entities/notifications'

    @Component
    export default class CreateBanner extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        notifications:Notifications=new Notifications();

        save(){
            (this.$refs.notificationsForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                        type:'notifications/create',
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
                this.$emit('input',value);
            }
        }
        notificationsRule={
            notificationName:[{required: true,message:this.L('FieldIsRequired',undefined,this.L('Name')),trigger: 'blur'}]
        }
    }
</script>

