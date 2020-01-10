import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);
import app from './modules/app'
import session from './modules/session'
import account from './modules/account'
import user from './modules/user'
import role from './modules/role'
import tenant from './modules/tenant'
import labaOrder from './modules/laba-order'
import banner from './modules/banner'
import article from "@/store/modules/article";
import shopOrder from "@/store/modules/shop-order";
import notifications from "@/store/modules/notifications";
import notificationSubscriptionInfo from "@/store/modules/notifications-subscription-info";

const store = new Vuex.Store({
    state: {
        //
    },
    mutations: {
        //
    },
    actions: {

    },
    modules: {
        app,
        session,
        account,
        user,
        role,
        tenant,
        labaOrder,
        banner,
        article,
        shopOrder,
        notifications,
        notificationSubscriptionInfo
    }
});

export default store;