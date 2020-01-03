import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import ShopOrder from '../entities/shop-order'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface ShopOrderState extends ListState<ShopOrder>{

}
class ShopOrderMutations extends ListMutations<ShopOrder>{

}
class UserModule extends ListModule<ShopOrderState,any,ShopOrder>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<ShopOrder>(),
        loading:false,
    }
    actions={
        async getAll(context:ActionContext<ShopOrderState,any>,payload:any){
            context.state.loading=true;
            let response=await Ajax.get('/api/services/app/ShopOrder/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=response.data.result as PageResult<ShopOrder>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<ShopOrderState,any>,payload:any){
            await Ajax.post('/api/services/app/ShopOrder/Create',payload.data);
        }
    };
    mutations={
        setCurrentPage(state:ShopOrderState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:ShopOrderState,pagesize:number){
            state.pageSize=pagesize;
        }
    }
}
const shopOrderModule=new UserModule();
export default shopOrderModule;