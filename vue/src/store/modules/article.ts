import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import Article from "@/store/entities/article";

interface ArticleState extends ListState<Article>{
    editArticle: Article,
    articleType: Array<any>
}

class ArticleModule extends ListModule<ArticleState,any,Article>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Article>(),
        loading:false,
        articleType: new Array()
    }
    actions={
        async getAll(context:ActionContext<ArticleState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Article/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Article>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<ArticleState,any>,payload:any){
            await Ajax.post('/api/services/app/Article/Create',payload.data);
        },
        async update(context:ActionContext<ArticleState,any>,payload:any){
            await Ajax.put('/api/services/app/Article/Update',payload.data);
        },
        async delete(context:ActionContext<ArticleState,any>,payload:any){
            await Ajax.delete('/api/services/app/Article/Delete?Id='+payload.data.id);
        },
        async getArticleType(context:ActionContext<ArticleState,any>){
            let reponse = await Ajax.get('/api/services/app/Article/GetArticleType');
            context.state.articleType=reponse.data.result;
        }
    };
    mutations={
        setCurrentPage(state:ArticleState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:ArticleState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:ArticleState,article:Article){
            state.editArticle=article;
        }
    }
}
const articleModule=new ArticleModule();
export default articleModule;