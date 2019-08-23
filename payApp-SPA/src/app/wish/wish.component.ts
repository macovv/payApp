import { Component, OnInit } from '@angular/core';
import { WishService } from '../services/wish.service';
import { Wish } from '../Models/wish';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-wish',
  templateUrl: './wish.component.html',
  styleUrls: ['./wish.component.css']
})
export class WishComponent implements OnInit {

  constructor(private wishService: WishService, private route: ActivatedRoute) { }

  wish: Wish;
  id: number;

  ngOnInit() {
    this.loadWish();
  }

  loadWish() {
    console.log(this.route.snapshot.params.id);
    this.wishService.getWish(this.route.snapshot.params.id).subscribe((res: Wish) => {
      this.wish = res;
    });
  }

}
