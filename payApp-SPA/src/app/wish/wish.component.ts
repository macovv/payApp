import { Component, OnInit } from '@angular/core';
import { WishService } from '../services/wish.service';
import { Wish } from '../Models/wish';

@Component({
  selector: 'app-wish',
  templateUrl: './wish.component.html',
  styleUrls: ['./wish.component.css']
})
export class WishComponent implements OnInit {

  constructor(private wishService: WishService) { }

  wish: Wish;

  ngOnInit() {
  }

  loadWish() {
    // wish = this.wishService.getWish();
  }

}
