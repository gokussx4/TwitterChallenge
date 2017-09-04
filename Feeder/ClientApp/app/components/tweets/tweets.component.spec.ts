/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { TweetsComponent } from './tweets.component';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';

let fixture: ComponentFixture<TweetsComponent>;

describe('Tweets component', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({ declarations: [TweetsComponent] });
        fixture = TestBed.createComponent(TweetsComponent);
        fixture.detectChanges();
    });

    it('should have an empty search input', async(() => {
        const searchText = fixture.nativeElement.querySelector('#search');
        expect(searchText).toEqual('Counter');
    }));

    it('should start with count 0, then increments by 1 when clicked', async(() => {
        const countElement = fixture.nativeElement.querySelector('strong');
        expect(countElement.textContent).toEqual('0');

        const incrementButton = fixture.nativeElement.querySelector('button');
        incrementButton.click();
        fixture.detectChanges();
        expect(countElement.textContent).toEqual('1');
    }));
});