import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class AppService {
  searchInput = new BehaviorSubject("");
  gridSize = new BehaviorSubject(false);
  breadcrumbMenu = new BehaviorSubject<any>([]);
  identity:
    | {
        isAdmin: boolean;
        name: string;
      }
    | any = null;
  constructor(private http: HttpClient) {}
  getIdentity() {
    return new Observable((sub) => {
      this.http.get<any>(`${environment.apiUrl}/user`).subscribe(
        (res) => {
          this.identity = res;
          sub.complete();
        },
        (error) => {
          sub.error();
        }
      );
    });
  }
}
