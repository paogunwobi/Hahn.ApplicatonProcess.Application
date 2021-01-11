import { Router } from 'aurelia-router';
import { autoinject, bindable } from 'aurelia-framework';
import { ApplicantService } from '../services/applicantservice';

@autoinject
export class Displayapplicants {
  @bindable applicants: any[];
  
  constructor(private applicantService: ApplicantService, private router: Router) {
  }
  
  async activate() {
    this.applicants = await this.applicantService.getApplicants();
  }

  // async attached() {
  //   console.log('Applicants Attached: ', this.applicants);
  // }

  async removeApplicant(id) {
    await this.applicantService.deleteApplicant(id);
    this.router.navigateToRoute('applicants');
  }
}
