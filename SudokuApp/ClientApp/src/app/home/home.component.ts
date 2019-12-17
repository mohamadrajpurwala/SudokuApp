import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {

  defaultBoard: any;
  ResultBoard: any;
  baseUrl: string;
  loading: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.Create();
  }

  Create(): any {
    this.http.get<any>(this.baseUrl + 'api/SampleData/Create').subscribe(result => {
      this.defaultBoard = result;
      this.ResultBoard = result;
    }, error => console.error(error));
  }

  Solve(): any {
    this.loading = true;
    this.http.post<any>(this.baseUrl + 'api/SampleData/Solve', null).subscribe(result => {
      this.ResultBoard = result;
    }, error => console.error(error),
      () => { this.loading = false});
  }
}
