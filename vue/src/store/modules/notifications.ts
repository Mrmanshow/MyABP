import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import Notifications from "@/store/entities/notifications";

interface NotificationsState extends ListState<Notifications>{
    editNotifications: Notifications
}

class NotificationsModule extends ListModule<NotificationsState,any,Notifications>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Notifications>(),
        loading:false,
        editNotifications: new Notifications()
    }
    actions={
        async getAll(context:ActionContext<NotificationsState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/NotificationsTemplate/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Notifications>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<NotificationsState,any>,payload:any){
            await Ajax.post('/api/services/app/NotificationsTemplate/Create',payload.data);
        },
        async update(context:ActionContext<NotificationsState,any>,payload:any){
            await Ajax.put('/api/services/app/NotificationsTemplate/Update',payload.data);
        },
        async delete(context:ActionContext<NotificationsState,any>,payload:any){
            await Ajax.delete('/api/services/app/NotificationsTemplate/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<NotificationsState,any>,payload:any){
            let response = await Ajax.get('/api/services/app/NotificationsTemplate/Get?Id='+payload);
            context.state.editNotifications = response.data.result;
        },
    };
    mutations={
        setCurrentPage(state:NotificationsState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:NotificationsState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:NotificationsState,notifications:Notifications){
            state.editNotifications=notifications;
        }
    }
}
const notificationsModule=new NotificationsModule();
export default notificationsModule;