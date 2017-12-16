import { SubsctiptionsRegistry } from './../services/SubsctiptionsRegistry';
import { InteropServiceFactory, RegistryUrls } from '../services/InteropServiceFactory';
import { Application } from './../../../../broker/dist/main/src/metadata/apps/model/Application.d';
import { Subscription } from 'rxjs/Subscription';
import { AppActions } from '../services/app.actions';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/operator/combineLatest';
import { Router } from '@angular/router';
import { ViewChild, ElementRef } from '@angular/core';
import { Store } from '@ngrx/store';
import * as fromRoot from '../services/reducers';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [SubsctiptionsRegistry]
})
export class HeaderComponent implements OnInit, OnDestroy {
  metadataUrl$: Observable<string>;
  connected$: Observable<boolean>;
  application$: Observable<Application>;

  public currentApp: Application;
  metadataUrls: RegistryUrls;

  constructor(
    private actions: AppActions,
    private store: Store<fromRoot.State>,
    private router: Router,
    private interopServiceFactory: InteropServiceFactory,
    private subscriptions: SubsctiptionsRegistry,
    private modalService: NgbModal) {
  }

  ngOnInit() {
    this.metadataUrl$ = this.store.select(state => state.plexus.metadataUrl);
    this.application$ = this.store.select(state => state.plexus.connectedApp);
    this.connected$ = this.store.select(state => state.plexus.connected);
    
    this.subscriptions.add(this.application$.subscribe(app => this.currentApp = app));
    this.subscriptions.add(this.metadataUrl$.combineLatest(this.connected$.filter(i => i)).subscribe(([metadata, _]) => {
      this.metadataUrls = this.interopServiceFactory.getMetadataUrls(metadata);
    }));
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribeAll();
  }

  disconnectFromPlexus() {
    this.store.dispatch({ type: AppActions.DISCONNECT_FROM_PLEXUS });
    this.router.navigate(['/home']);
  }

  disconnectFromApp() {
    this.store.dispatch({ type: AppActions.DISCONNECT_FROM_APP });
    this.router.navigate(['/apps']);
  }

  openCurrentApp() {
    if (this.currentApp) {
      this.store.dispatch({ type: AppActions.CONNECT_TO_APP, payload: this.currentApp });
      this.router.navigate(['/app']);
    }
  }

  openModal(content) {
    this.modalService.open(content);
  }
}
