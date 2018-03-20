import { Component, OnInit } from '@angular/core';
import { WeaponService } from '../../services/weapon/weapon.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';


@Component({
  selector: 'app-weapon-form',
  templateUrl: './weapon-form.component.html',
  styleUrls: ['./weapon-form.component.css']
})
export class WeaponFormComponent implements OnInit {

  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];

  weapon = {
    makeId: {},
    modelId: {},
    isRegistered: {},
    features: [,],
    contact: {
      name: '',
      email: '',
      phone: ''
    }
  };

  constructor(private activatedRoute: ActivatedRoute,private router: Router, private _weapon: WeaponService) { 
    this.weapon.features = [];
    activatedRoute.params.subscribe( param => {
      this.weapon.makeId = param['id'];
    });
  }

  ngOnInit() {

    let source =[
      this._weapon.getMakes(),
      this._weapon.getFeatures(),
    ]

    if (this.weapon.makeId) {      
      source.push(this._weapon.getWeapon(this.weapon.makeId));
    }
    

    Observable.forkJoin(
      source

      /* Otra manera es crear el Observable[] Ex:source */
      // [
      //   this._weapon.getMakes(),
      //   this._weapon.getFeatures(),
      //   this._weapon.getWeapon(this.weapon.makeId)
      // ]
    ).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];

      if (this.weapon.makeId) 
        this.weapon = data[2];
        
    }, err => {
      if (err.status == 404) {
            // this.router.navigate(['/not-found']);
            this.router.navigate(['/home']);
          }
    });

    /* La manera de arriba es mas limpia y eficiente */

    // this._weapon.getWeapon(this.weapon.makeId).subscribe( w => {
    //   this.weapon = w;
    // }, err => {
    //   if (err.status == 404) {
    //     // this.router.navigate(['/not-found']);
    //     this.router.navigate(['/home']);
    //   }
    // });

    // this._weapon.getMakes().subscribe(
    //   res => {
    //     this.makes = res;
    //     console.log(this.makes, `OnInit()`);
    //   }
    // );

    // this._weapon.getFeatures().subscribe(
    //   res => {
    //     this.features = res;
    //     console.log(this.features, `OnInit()`);
    //   }
    // );

  }

  onMakeChange() {

    if (this.weapon.makeId > 0) {
      console.log(this.weapon.makeId);
      let make = this.makes.find(m => m.id == this.weapon.makeId);
      this.weapon.modelId = {};
      console.log(this.weapon);

      this.models = make.models;
      console.log(this.models);
    }
    else {
      this.models = [];
    }

  }
  onFeatureChange(id: any, event: any) {    

    if (!event.target.checked) {
      var index = this.weapon.features.indexOf(id);
      this.weapon.features.splice(index, 1);
    }
    else {
      this.weapon.features.push(id);
    }
    console.log(id, event);
  }

  submit(form: NgForm) {
    console.log(`Formulario`, form);
    this._weapon.createWeapon(this.weapon)
      .subscribe(
        x => console.log(x),
        e => {
          // Mostrar el error en un toast por ejemplo
          console.error(e);
        }
      );
  }

}
