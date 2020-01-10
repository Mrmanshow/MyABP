<template>
    <div>
        <Card dis-hover>
            <div class="page-body">
                <Form ref="queryForm" :label-width="80" label-position="left" inline>
                    <Row :gutter="16">
                        <Col span="6">
                        <FormItem :label="L('Keyword')+':'" style="width:100%">
                            <Input v-model="pagerequest.keyword" :placeholder="L('Banner')+'/'+L('Name')"></Input>
                        </FormItem>
                        </Col>
                    </Row>
                    <Row>
                        <Button icon="ios-search" type="primary" size="large" @click="getpage">{{L('Find')}}</Button>
                        <Button @click="create" icon="android-add" type="primary" size="large" class="toolbar-btn">{{L('Add')}}</Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                    </Table>
                    <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                </div>
            </div>
        </Card>

        <create-order v-model="createModalShow" @save-success="getpage" ></create-order>
    </div>
</template>
<script lang="ts">
    import { Component } from 'vue-property-decorator';
    import Util from '@/lib/util'
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'
    import CreateOrder from './create-order.vue'

    class  PageArticleRequest extends PageRequest{
        keyword:string;
    }

    @Component({
        components: { CreateOrder }
    })
    export default class ShopOrder extends AbpBase{
        //filters
        pagerequest:PageArticleRequest=new PageArticleRequest();

        createModalShow:boolean=false;

        get list(){
            return this.$store.state.shopOrder.list;
        };
        get loading(){
            return this.$store.state.shopOrder.loading;
        };
        // get articleType(){
        //     return this.$store.state.article.articleType;
        // };
        get pageSize(){
            return this.$store.state.shopOrder.pageSize;
        }
        get totalCount(){
            return this.$store.state.shopOrder.totalCount;
        }
        get currentPage(){
            return this.$store.state.shopOrder.currentPage;
        }

        async created(){
            this.getpage();
            // await this.$store.dispatch({
            //     type:'article/getArticleType'
            // })
        }

        create(){
            this.createModalShow=true;
        }
        pageChange(page:number){
            this.$store.commit('shopOrder/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('shopOrder/setPageSize',pagesize);
            this.getpage();
        }
        async getpage(){
            this.pagerequest.maxResultCount=this.pageSize;
            this.pagerequest.skipCount=(this.currentPage-1)*this.pageSize;

            //filters

            await this.$store.dispatch({
                type:'shopOrder/getAll',
                data:this.pagerequest
            })
        }

        columns=[
            {
                title:this.L('OrderCode'),
                key:'orderCode'
            },{
                title:this.L('ProductName'),
                key:'productName'
            },{
                title:this.L('Count'),
                key:'count'
            },{
                title:this.L('FullName'),
                key:'name'
            },{
                title:this.L('Address'),
                key:'detailAddress'
            },{
                title:this.L('PhoneNumber'),
                key:'phone'
            },{
                title:this.L('ExpressCode'),
                key:'expressCode'
            },{
                title:this.L('Amount'),
                key:'amount'
            },{
                title:this.L('Status'),
                render:(h:any,params:any)=>{
                    return h('span',params.row.status?this.L('No'):this.L('Yes'))
                }
            },{
            title:this.L('CreationTime'),
            render:(h:any,params:any)=>{
                return h('span',Util.dateFormat(new Date(params.row.creationTime), 'yyyy-MM-dd HH:mm:ss') )
            }
        }]


    }
</script>