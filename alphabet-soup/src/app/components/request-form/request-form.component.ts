import { Component, OnInit } from '@angular/core';
import { sopa } from '../../data/default-data';
import { AlphabetModel } from '../../models/alphabet-model';
import { ApiResult } from '../../models/api-result';
import { AlphabetService } from '../../services/alphabet.service';

@Component({
  selector: 'app-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss']
})
export class RequestFormComponent implements OnInit {

  soup: string = "";
  word: string = "TIBURÓN";
  size: number = sopa.length;

  loading: boolean = false;

  constructor(private alphabetService: AlphabetService) { }

  ngOnInit(): void {
    this.createSoup();
  }

  createSoup() {
    this.size = sopa.length;
    for (var i = 0; i < sopa.length; i++) {
      sopa[i].forEach(c => {
        this.soup += c;
      });
      this.soup += "\n";
    }
  }

  validate() {
    this.loading = true;
    const data: AlphabetModel = {
      AlphabetSoup: this.createStringsArray(),
      WordToFind: this.word,
      SoupSize: this.size
    };
    this.alphabetService.validateIfWordExists(data)
      .subscribe((response: ApiResult) => {
        if (response) {
          alert(JSON.stringify(response));
        }
        this.loading = false;
      }, (err) => {
        console.log(err);
        alert(JSON.stringify(err));
      });
  }

  createStringsArray(): string[] {
    return this.soup.split('\n');
  }

}
