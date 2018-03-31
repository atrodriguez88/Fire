import { Component, OnInit } from '@angular/core';
import { WeaponService } from '../../services/weapon/weapon.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';
import { ISaveWeapon, Iweapon } from '../../interfaces/iweapon';


@Component({
  selector: 'app-weapon-form',
  templateUrl: './weapon-form.component.html',
  styleUrls: ['./weapon-form.component.css']
})
export class WeaponFormComponent implements OnInit {

  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];

  weapon: ISaveWeapon = {
    id: -1,
    modelId: 0,
    makeId: 0,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: ''
    },
    isRegistered: false
  };

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private _weapon: WeaponService) {
    this.weapon.features = [];
    activatedRoute.params.subscribe(param => {
      this.weapon.id = param['id'];
    });
  }

  ngOnInit() {

    let source = [
      this._weapon.getMakes(),
      this._weapon.getFeatures(),
    ]

    if (this.weapon.id) {
      source.push(this._weapon.getWeapon(this.weapon.id));
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

      if (this.weapon.id) {
        console.log(data[0]);
        this.setWeapon(data[2]);

        this.onMakeChange();
      }

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

  setWeapon(w: Iweapon) {
    this.weapon.id = w.id;
    this.weapon.makeId = w.make.id;
    this.weapon.modelId = w.model.id;
    this.weapon.features = w.features;
    this.weapon.contact = w.contact;
    this.weapon.isRegistered = w.isRegistered;
  }

  onMakeChange() {
    console.log('checkeee');


    if (this.weapon.makeId > 0) {
      console.log(this.weapon.makeId);
      let make = this.makes.find(m => m.id == this.weapon.makeId);
      // this.weapon.modelId = 0;
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

  delete() {
    // Confirm Toast here
    this._weapon.deleteWeapon(this.weapon.id).subscribe(
      res => {
        this.router.navigate(['/home']);
      }
    );
  }

  submit(form: NgForm) {
    console.log(`Formulario`, form);

    if (this.weapon.id) {
      this._weapon.updateWeapon(this.weapon)
        .subscribe(
          //Toast Here
        );
    }
    else {
      this._weapon.createWeapon(this.weapon)
        .subscribe(
          x => {
            console.log(x);
            this.router.navigate(['/weapons', this.weapon.id])
          },
          e => {
            // Mostrar el error en un toast por ejemplo
            console.error(e);
          }
        );
    }

  }

}
