
import { PLATFORM } from 'aurelia-pal';
import { RouterConfiguration, Router } from "aurelia-router";
import {I18N} from 'aurelia-i18n';
import {EventAggregator} from 'aurelia-event-aggregator';


export class App {
  currentLocale: string = '';
  constructor(private i18n: I18N, private ea: EventAggregator, private element: HTMLElement) { 
    // this.currentLocale = this.i18n.getLocale();
    // this.ea.subscribe('i18n:locale:changed', payload => {
    //   this.i18n.updateTranslations(this.element);
    // });
  }
  configureRouter(config: RouterConfiguration, router: Router) {
    config.options.pushState = true;

    config.map([
      { route: ['', 'applicant/:id?'], name: 'applicant', title: 'Applicant', moduleId: PLATFORM.moduleName('pages/saveapplicant'), nav: true },
      { route: 'applicants', name: 'applicants', title: 'Applicants', moduleId: PLATFORM.moduleName('pages/displayapplicants'), nav: true }
    ]);  
  }
}
