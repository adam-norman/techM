import { MyRequestsComponent } from './pages/employee/my-requests/my-requests.component';
import { RequestFormComponent } from './pages/employee/request-form/request-form.component';
import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {MainComponent} from '@modules/main/main.component';
import {LoginComponent} from '@modules/login/login.component';
import {DashboardComponent} from '@pages/dashboard/dashboard.component';
import {AuthGuard} from '@guards/auth.guard';
import {NonAuthGuard} from '@guards/non-auth.guard';
import { UpdateRequestsComponent } from '@pages/admin/update-requests/update-requests.component';


const routes: Routes = [
    {
        path: '',
        component: MainComponent,
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        children: [
            {
                path: 'new-request',
                component: RequestFormComponent
            },
            {
              path: 'my-requests',
              component: MyRequestsComponent
          },{
            path: 'update-requests',
            component: UpdateRequestsComponent
        },
            {
                path: '',
                component: DashboardComponent
            }
        ]
    },
    {
        path: 'login',
        component: LoginComponent,
        canActivate: [NonAuthGuard]
    },
    {path: '**', redirectTo: ''}
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {relativeLinkResolution: 'legacy'})],
    exports: [RouterModule]
})
export class AppRoutingModule {}
