import { Component, OnInit } from '@angular/core';
import { WishService } from 'src/app/services/wish.service';
import { Wish } from 'src/app/Models/wish';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {

  wishes: Wish[];

  constructor(private wishService: WishService) { }

  ngOnInit() {
    this.loadWishes();
    // console.log(this.wishes);
  }

  loadWishes() {
    this.wishService.getWishes().subscribe((res: Wish[]) => {
      this.wishes = res;
      // console.log('w ' + this.wishes);
      // console.log('w2 ' + res);
      // console.log('ciota pizda');
      console.log(this.wishes);
      this.wishes = res;
    });
  }
}
