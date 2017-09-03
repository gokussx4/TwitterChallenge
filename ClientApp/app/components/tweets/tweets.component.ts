import { Component, Inject, OnDestroy } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { ISubscription } from 'rxjs/Subscription';

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
        this.tweets.forEach((tweet) => {
            tweet.isVisible = tweet.message.indexOf(this.filterText) > -1;
        });
    }

    public tweets: Tweet[];
    public filterText: string;
    private subscription: ISubscription;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        let url = baseUrl + 'api/TweeterFeed';

        this.subscription = Observable.timer(0, 60000)
            .switchMap(() => http.get(url))
            .subscribe(result => {
                this.tweets = result.json() as Tweet[];
                this.tweets.forEach((tweet) => {
                    tweet.isVisible = true;
                });

                console.info('tweets set!');
            }, error => console.error(error));
    }    
}

interface Tweet {
    createdOn: Date;
    imageUrls: string[];
    message: string;
    retweetCount: number;
    userName: string;
    userProfileImageUrl: string;
    userScreenName: string;
    isVisible: boolean;
}
