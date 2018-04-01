import { Component, OnInit, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Iweapon } from '../../interfaces/iweapon';
import { WeaponService } from '../../services/weapon/weapon.service';
import { PhotoService } from '../../services/photo.service';

@Component({
  selector: 'app-weapon-details',
  templateUrl: './weapon-details.component.html',
  styleUrls: ['./weapon-details.component.css']
})
export class WeaponDetailsComponent implements OnInit {

  weaponId: any;
  weapon: any;

  constructor(private router: Router, private activated: ActivatedRoute,
    private _weapon: WeaponService, private _photo: PhotoService) {

    this.activated.params.subscribe(
      param => {
        this.weaponId = param['id'];
      }
    );

  }

  ngOnInit() {
    this._weapon.getWeapon(this.weaponId).subscribe(
      res => {
        console.log(res);
        this.weapon = res;
      }
    );
  }

  deleteWeapon() {
    this._weapon.deleteWeapon(this.weaponId).subscribe(
      res => {
        // Borrado satifactorio
        console.log("Borrado Satifactorio");
        this.router.navigate(['/weapons']);
      },
      err => {
        console.log("No Borrado Satifactorio");
        this.router.navigate(['/home']);
      }
    )
  }

  uploadPhoto(event: any) {

    console.log(event);

    var nativeElement: any = event.target;

    this._photo.UploadPhoto(this.weaponId, nativeElement.files[0]).subscribe(res => {
      console.log(res);
    });

    /* Usando AJAX */
    // this._photo.UpLoad(this.weaponId, nativeElement.files[0]);
  }

}
