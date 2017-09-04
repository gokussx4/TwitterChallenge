import { Component, Inject, OnDestroy } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { ISubscription } from 'rxjs/Subscription';
import * as moment from 'moment';

@Component({
    selector: 'tweets',
    templateUrl: './tweets.component.html'
})
export class TweetsComponent implements OnDestroy {
    ngOnDestroy(): void {
        this.subscription.unsubscribe();
        console.info('clear!');
    }

    filter(): void {
        console.log(this.filterText);
        this.tweets.forEach((tweet) => {
            tweet.isVisible = tweet.message.indexOf(this.filterText) > -1;
        });
    }

    public tweets: Tweet[];
    public filterText: string = '';
    private subscription: ISubscription;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        let url = baseUrl + 'api/TweeterFeed';

        this.getData(http, url);
    }    

    getData(http: Http, url: string): void {
        this.subscription = Observable.timer(0, 60000)
            .switchMap(() => http.get(url))
            .subscribe(result => {
                this.tweets = result.json() as Tweet[];
                this.tweets.forEach((tweet) => {
                    tweet.isVisible = true;
                    tweet.fromNow = moment.utc(tweet.createdOn).fromNow();
                });
                this.filter();
                console.info('tweets set!');
            }, error => console.error(error));
    }
}

interface Tweet {
    createdOn: Date;
    fromNow: string;
    imageUrls: string[];
    message: string;
    retweetCount: number;
    userName: string;
    userProfileImageUrl: string;
    userScreenName: string;
    isVisible: boolean;
}
