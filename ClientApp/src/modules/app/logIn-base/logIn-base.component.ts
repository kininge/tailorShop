import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-logIn-base',
    templateUrl: './logIn-base.component.html',
    styleUrls: ['./logIn-base.component.scss']
})
export class LogInBaseComponent implements OnInit {

    constructor(private router: Router) { }

    ngOnInit()
    {
        this.router.navigateByUrl('/logIn');
    }
}
