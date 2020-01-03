<template>
    <div>
        <Modal
         :title="L('CreateNewOrder')"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="orderForm"  label-position="top" :rules="orderRule" :model="order">
                <Tabs value="detail">
                    <TabPane :label="L('UserDetails')" name="detail">

                        <FormItem :label="L('ProductId')" prop="productId">
                            <InputNumber :min="0" :precision="0" v-model="order.productId" type="number"></InputNumber>
                        </FormItem>
                        <FormItem :label="L('Count')" prop="count">
                            <InputNumber :min="1" :precision="0" v-model="order.count" type="number"></InputNumber>
                        </FormItem>
                        <FormItem :label="L('PayIntegration')" prop="payIntegration">
                            <InputNumber :min="0" :precision="0" v-model="order.payIntegration" type="number"></InputNumber>
                        </FormItem>
                        <FormItem :label="L('Idx')" prop="idx">
                            <InputNumber :min="0" :precision="0" v-model="order.idx" type="number"></InputNumber>
                        </FormItem>
                        <FormItem :label="L('AddressId')" prop="addressId">
                            <InputNumber :min="0" :precision="0" v-model="order.addressId" type="number"></InputNumber>
                        </FormItem>
                        <FormItem :label="L('ContactWay')" prop="ContactWay">
                            <Input v-model="order.ContactWay" :maxlength="100"></Input>
                        </FormItem>
                        <FormItem :label="L('Remarks')" prop="remarks">
                            <Input v-model="order.remarks" :maxlength="100"></Input>
                        </FormItem>
                        <FormItem :label="L('PayPassword')" prop="payPassword">
                            <Input v-model="order.payPassword" :maxlength="30"></Input>
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
    import ShopOrder from '../../../store/entities/shop-order'
    import url from '../../../lib/url'

    @Component
    export default class CreateShopOrder extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        order:ShopOrder=new ShopOrder();

        save(){
            (this.$refs.orderForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                        type:'shopOrder/create',
                        data:this.order
                    });
                    (this.$refs.orderForm as any).resetFields();
                    this.$emit('save-success');
                    this.$emit('input',false);
                }
            })
        }
        cancel(){
            (this.$refs.orderForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.$emit('input',value);
            }
        }

        orderRule={
            payPassword:[{required: true,message:this.L('FieldIsRequired',undefined,this.L('PayPassword')),trigger: 'blur'}]
        }
    }
</script>

