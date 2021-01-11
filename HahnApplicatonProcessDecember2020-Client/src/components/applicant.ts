import { Router } from 'aurelia-router';

import { ApplicantService } from './../services/applicantservice';
import { bindable } from 'aurelia-framework';

export class Applicant {
  @bindable applicant;
  id: number;
  isHovering = false;

  constructor(private applicantService: ApplicantService, private router: Router) { 
    
  }

  activate() {
    this.applicant;
    console.log('Applicant: ', this.applicant);
  }

  async getApplicant(id) {
    this.applicant = await this.applicantService.getApplicant(id);
  }

  mouseOver() {
    this.isHovering = true;
  }

  mouseOut() {
    this.isHovering = false;
  }

  async updateApplicant(id) {
    // await this.applicantService.putApplicant(applicant.id, applicant);
    this.id = id;
    this.router.navigateToRoute('applicant', {id: this.id});
  }

  async removeApplicant(id) {
    await this.applicantService.deleteApplicant(id);
    this.router.navigateToRoute('applicants');
  }
}
