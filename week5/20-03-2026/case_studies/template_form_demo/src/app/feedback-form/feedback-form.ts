import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule , NgForm} from '@angular/forms';

@Component({
  selector: 'app-feedback-form',
  imports: [CommonModule, FormsModule],
  templateUrl: './feedback-form.html',
  styleUrl: './feedback-form.css',
})
export class FeedbackForm {
  departments = ['HR', 'Development', 'Design' , 'QA'];
  allskills = ['Angular', 'React', 'NodeJS' , 'Python'];

  feedback = {
    name : '',
    email : '',
    department : '',
    rating : '',
    comments : '',
    skills : [] as string[],
  };

  submitForm(form : NgForm) {
    if(form.valid) {
      console.log('Feedback submitted:', this.feedback);
      alert(JSON.stringify(this.feedback , null , 2));
      form.resetForm();
      this.feedback.skills = [];
    }
    else{
      alert('Please fill all required fields correctly.');
    }
  }

  updateSkills(skill : string , isChecked : boolean) {
    if(isChecked) {
      this.feedback.skills.push(skill);
    }
    else{
      const index = this.feedback.skills.indexOf(skill);
      if(index >= 0) {
        this.feedback.skills.splice(index , 1);
      }
    }
  }
}
