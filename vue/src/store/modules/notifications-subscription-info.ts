import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import NotificationSubscriptionInfo from "@/store/entities/notifications-subscription-info";

interface NotificationSubscriptionInfoState extends ListState<NotificationSubscriptionInfo>{

}

class NotificationSubscriptionInfoModule extends ListModule<NotificationSubscriptionInfoState,any,NotificationSubscriptionInfo>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<NotificationSubscriptionInfo>(),
        loading:false
    }
    actions={
        async getAll(context:ActionContext<NotificationSubscriptionInfoState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/NotificationSubscriptionInfo/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<NotificationSubscriptionInfo>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async unsubscribe(context:ActionContext<NotificationSubscriptionInfoState,any>,payload:any){
            await Ajax.post('/api/services/app/NotificationSubscriptionInfo/Unsubscribe', payload.data);
        },
    };
    mutations={
        setCurrentPage(state:NotificationSubscriptionInfoState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:NotificationSubscriptionInfoState,pagesize:number){
            state.pageSize=pagesize;
        }
    }
}
const notificationSubscriptionInfoModule=new NotificationSubscriptionInfoModule();
export default notificationSubscriptionInfoModule;