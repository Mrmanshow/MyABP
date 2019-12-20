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

        <create-article v-model="createModalShow"  @save-success="getpage"></create-article>
        <edit-article v-model="editModalShow"  @save-success="getpage"></edit-article>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import url from '../../../lib/url'
    import Util from '@/lib/util'
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'
    import CreateArticle from './create-article.vue'
    import EditArticle from './edit-article.vue'

    class  PageArticleRequest extends PageRequest{
        keyword:string;
        from:Date;
        to:Date;
    }

    @Component({
        components:{CreateArticle,EditArticle}
    })
    export default class LabaOrder extends AbpBase{
        //filters
        pagerequest:PageArticleRequest=new PageArticleRequest();
        creationTime:Date[]=[];
        createModalShow:boolean=false;
        editModalShow:boolean=false;

        get list(){
            return this.$store.state.article.list;
        };
        get loading(){
            return this.$store.state.article.loading;
        };
        get articleType(){
            return this.$store.state.article.articleType;
        };
        get pageSize(){
            return this.$store.state.article.pageSize;
        }
        get totalCount(){
            return this.$store.state.article.totalCount;
        }

        edit(){
            this.editModalShow=true;
        }
        create(){
            this.createModalShow=true;
        }
        pageChange(page:number){
            this.$store.commit('article/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('article/setPageSize',pagesize);
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
                type:'article/getAll',
                data:this.pagerequest
            })
        }

        get currentPage(){
            return this.$store.state.article.currentPage;
        }
        columns=[{
            title:this.L('Type'),
            render:(h:any,params:any)=>{
                let type = params.row.type;
                let name = '';
                this.articleType.forEach((item) => {
                    if (item.value == type) {
                        name = item.name
                        return;
                    }
                })
                return h('span',this.L(name))
            }
        },{
            title:this.L('Title'),
            key:'title'
        },{
            title:this.L('Img'),
            render:(h:any,params:any)=>{
            return  h('img',{
                attrs: {
                    src: `${ url }Upload/Article/${ params.row.img }`,
                    style: "width: 50px;"
                }
            })
        }
        },{
            title:this.L('CreationTime'),
            render:(h:any,params:any)=>{
                return h('span',Util.dateFormat(new Date(params.row.creationTime), 'yyyy-MM-dd HH:mm:ss') )
            }
        },{
            title:this.L('Status'),
            render:(h:any,params:any)=>{
                return h('span',params.row.status?this.L('No'):this.L('Yes'))
            }
        },{
            title:this.L('Sequence'),
            key:'sort'
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
                                this.$store.commit('article/edit',params.row);
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
                                    content:this.L('DeleteBannerConfirm'),
                                    okText:this.L('Yes'),
                                    cancelText:this.L('No'),
                                    onOk:async()=>{
                                        await this.$store.dispatch({
                                            type:'article/delete',
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
            await this.$store.dispatch({
                type:'article/getArticleType'
            })
        }
    }
</script>