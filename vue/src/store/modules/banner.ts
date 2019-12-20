import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import Banner from "@/store/entities/banner";

interface BannerState extends ListState<Banner>{
    bannerType: Array<any>
    editBanner: Banner
}

class BannerModule extends ListModule<BannerState,any,Banner>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Banner>(),
        loading:false,
        bannerType: Array<string>(),
        editBanner: new Banner()
    }
    actions={
        async getAll(context:ActionContext<BannerState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Banner/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Banner>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<BannerState,any>,payload:any){
            await Ajax.post('/api/services/app/Banner/Create',payload.data);
        },
        async update(context:ActionContext<BannerState,any>,payload:any){
            await Ajax.put('/api/services/app/Banner/Update',payload.data);
        },
        async delete(context:ActionContext<BannerState,any>,payload:any){
            await Ajax.delete('/api/services/app/Banner/Delete?Id='+payload.data.id);
        },
        async getBannerType(context:ActionContext<BannerState,any>){
            let reponse = await Ajax.get('/api/services/app/Banner/GetBannerType');
            context.state.bannerType=reponse.data.result;
        }
    };
    mutations={
        setCurrentPage(state:BannerState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:BannerState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:BannerState,banner:Banner){
            state.editBanner=banner;
        }
    }
}
const bannerModule=new BannerModule();
export default bannerModule;