import { Component, OnInit } from '@angular/core';
import { ciertas, falsas, sopa } from '../../data/default-data';
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
  collapse: boolean = true;
  results: ApiResult[] = [];

  constructor(private alphabetService: AlphabetService) { }

  ngOnInit(): void {
    this.createSoup();
    this.validateAllWords();
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
          this.results.push(response);
        }
        this.loading = false;
      }, (err) => {
        console.log(err);
        alert(JSON.stringify(err));
      });
  }

  validateWord(value: string) {
    const data: AlphabetModel = {
      AlphabetSoup: this.createStringsArray(),
      WordToFind: value,
      SoupSize: this.size
    };
    this.alphabetService.validateIfWordExists(data)
      .subscribe((response: ApiResult) => {
        if (response) {
          this.results.push(response);
        }
      }, (err) => {
        console.log(err);
        alert(JSON.stringify(err));
      });
  }

  createStringsArray(): string[] {
    return this.soup.split('\n');
  }

  switchCollapse() {
    this.collapse = !this.collapse;
  }

  validateAllWords() {
    let allWords: string[] = [];
    ciertas.forEach(w => {
      allWords.push(w);
    });
    falsas.forEach(w => {
      allWords.push(w);
    });
    this.loading = true;
    allWords.forEach(value => {
      this.validateWord(value);
    });
    this.loading = false;
  }

}
