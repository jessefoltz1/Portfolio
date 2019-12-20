import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home.vue'
import Login from '@/views/Login.vue'
import Register from '@/views/Register.vue'
import Dashboard from '@/views/Dashboard.vue'
import About from '@/views/About.vue'
import NewIncident from '@/views/NewIncident.vue'
import IncidentDetails from '@/views/IncidentDetails.vue'
import auth from '@/shared/auth'
import AddEmployeeOrAdmin from '@/views/AddEmployeeOrAdmin.vue'
Vue.use(VueRouter)

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,

  routes: [
      {
        path: '/home',
        name: 'home',
        component: Home
      },
      {
        path: '/about',
        name: 'about',
        component: About
      },
    
      {
        path: "/login",
        name: "login",
        component: Login
      },
      {
        path: "/register",
        name: "register",
        component: Register
      },
      {
        path: "/",
        name: "dashboard",
        component: Dashboard
      },
      {
        path: "/incident/new",
        name: "new-incident",
        component: NewIncident
      },
      {
        path: "/incident/:incident",
        name: "incident-details",
        component: IncidentDetails
      },
      {
        path: "/employee/add",
        name: "add-employee",
        component: AddEmployeeOrAdmin
      }

    ]


 
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login', '/register', '/home', '/about'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = auth.getUser();

  if (authRequired && !loggedIn) {
    return next('/home');
  }

  next();
});

export default router
