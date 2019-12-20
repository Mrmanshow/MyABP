import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import LabaOrder from '../entities/laba-order'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';

interface LabaOrderState extends ListState<LabaOrder>{

}

class LabaOrderModule extends ListModule<LabaOrderState,any,LabaOrder>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<LabaOrder>(),
        loading:false
    }
    actions={
        async getAll(context:ActionContext<LabaOrderState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/LabaOrder/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<LabaOrder>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async createLabaOrder(context:ActionContext<LabaOrderState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.post('/api/services/app/LabaOrder/CreateLabaOrder',payload.data);
            context.state.loading=false;
            let page=reponse.data.result
        }
    };
}
const labaOrderModule=new LabaOrderModule();
export default labaOrderModule;