import { Component, OnInit } from '@angular/core';
import { WeaponService } from '../../services/weapon/weapon.service';

@Component({
  selector: 'app-weapon-form',
  templateUrl: './weapon-form.component.html',
  styleUrls: ['./weapon-form.component.css']
})
export class WeaponFormComponent implements OnInit {

  makeId: any

  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];

  constructor(private _weapon: WeaponService) { }

  ngOnInit() {

    this._weapon.getMakes().subscribe(
      res => {
        this.makes = res;
        console.log(this.makes, `OnInit()`);
      }
    );

    this._weapon.getFeatures().subscribe(
      res => {
        this.features = res;
        console.log(this.features, `OnInit()`);
      }
    );

  }

  onChange() {

    if (this.makeId > 0) {
      console.log(this.makeId);
      let make = this.makes.find(m => m.id == this.makeId);
      console.log(make);
      this.models = make.models;
      console.log(this.models);
    }
    else
    {
      this.models = [];
    }

  }

}
