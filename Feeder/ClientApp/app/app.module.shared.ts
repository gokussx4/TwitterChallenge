import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { TweetsComponent } from './components/tweets/tweets.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        TweetsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'tweets', pathMatch: 'full' },
            { path: 'tweets', component: TweetsComponent },
            { path: '**', redirectTo: 'tweets' }
        ])
    ]
})
export class AppModuleShared {
}
