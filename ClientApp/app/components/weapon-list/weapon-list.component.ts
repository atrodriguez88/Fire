import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Iweapon } from '../../interfaces/iweapon';
import { WeaponService } from '../../services/weapon/weapon.service';
import { keyValuePair } from '../../interfaces/keyValuePair';

@Component({
  selector: 'app-weapon-list',
  templateUrl: './weapon-list.component.html',
  styleUrls: ['./weapon-list.component.css']
})
export class WeaponListComponent implements OnInit {

  makes: keyValuePair[] = [];
  weapons: Iweapon[] = [];
  allweapons: Iweapon[] = [];
  filter: any = {};

  constructor(private _weapon: WeaponService, router: Router) {

    this._weapon.getMakes().subscribe(res => {
      this.makes = res;
    });

    this.populateFilter();
  }

  ngOnInit() {

  }

  populateFilter(){
    this._weapon.getWeapons(this.filter).subscribe(
      res => {
        console.log(res);
        this.weapons = this.allweapons = res;
      });
  }

  onChangeFilter() {

    if (this.filter.makeId) {

      /* Filter for Server Side */
      this.populateFilter();
      
      /* This filter is for a Client Side. It's better to use for few data(100-1000 weapons) */

      // var weapons = this.allweapons;
      // weapons = weapons.filter(w => w.make.id == this.filter.makeId);

      // // I can implement other filter
      // if (this.filter.modelId) {
      //   weapons.filter(w => w.model.id == this.filter.modelId);
      // }
      // this.weapons = weapons;

    }


  }

  resetFilter() {
    this.filter = {};
    this.onChangeFilter();
  }

}
