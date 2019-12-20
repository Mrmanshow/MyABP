<template>
    <div>
        <Card dis-hover>
            <div class="page-body">
                <Form ref="queryForm" :label-width="80" label-position="left" inline>
                    <Row :gutter="16">
                        <Col span="6">
                        <FormItem :label="L('Keyword')+':'" style="width:100%">
                            <Input v-model="pagerequest.keyword" :placeholder="L('UserName')+'/'+L('Name')"></Input>
                        </FormItem>
                        </Col>
                        <Col span="6">
                        <FormItem :label="L('CreationTime')+':'" style="width:100%">
                            <DatePicker  v-model="creationTime" type="datetimerange" format="yyyy-MM-dd" style="width:100%" placement="bottom-end" :placeholder="L('SelectDate')"></DatePicker>
                        </FormItem>
                        </Col>
                    </Row>
                    <Row>
                        <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Find')}}</Button>
                        <Button icon="ios-search" type="primary" size="large" @click="labaOrder" class="toolbar-btn">下单</Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                    </Table>
                    <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                </div>
            </div>
        </Card>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '@/lib/util'
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'
    class  PageUserRequest extends PageRequest{
        keyword:string;
        from:Date;
        to:Date;
    }

    @Component({
        // components:{CreateUser,EditUser}
    })
    export default class LabaOrder extends AbpBase{
        //filters
        pagerequest:PageUserRequest=new PageUserRequest();
        creationTime:Date[]=[];

        get list(){
            return this.$store.state.labaOrder.list;
        };
        get loading(){
            return this.$store.state.labaOrder.loading;
        }
        pageChange(page:number){
            this.$store.commit('labaOrder/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('labaOrder/setPageSize',pagesize);
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
                type:'labaOrder/getAll',
                data:this.pagerequest
            })
        }
        async labaOrder() {
            // let index = 0;
            // let timer = setInterval(()=>{
                this.$store.dispatch({
                    type:'labaOrder/createLabaOrder',
                    data: {
                        Routes: '1-1|2-1|3-1|4-1|5-1|6-1|7-1|8-1|9-1'
                    }
                })
            //     index++;
            //     if(index === 1000){
            //         clearInterval(timer)
            //     }
            // },10)
        }
        get pageSize(){
            return this.$store.state.labaOrder.pageSize;
        }
        get totalCount(){
            return this.$store.state.labaOrder.totalCount;
        }
        get currentPage(){
            return this.$store.state.labaOrder.currentPage;
        }
        columns=[{
            title:this.L('UserName'),
            key:'userName'
        },{
            title:this.L('Amount'),
            key:'amount'
        },{
            title:this.L('WinAmount'),
            key:'winAmount'
        },{
            title:this.L('Position'),
            key:'position'
        },{
            title:this.L('CreationTime'),
            render:(h:any,params:any)=>{
                return h('span',new Date(params.row.creationTime).toLocaleString())
            }
        }]
        async created(){
            this.getpage();
        }
    }
</script>