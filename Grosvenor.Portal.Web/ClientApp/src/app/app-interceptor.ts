import { Injectable } from "@angular/core";
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpXsrfTokenExtractor,
  HttpErrorResponse,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { NzNotificationService } from "ng-zorro-antd/notification";
@Injectable()
export class AppInterceptor implements HttpInterceptor {
  constructor(
    private xsrf: HttpXsrfTokenExtractor,
    private notification: NzNotificationService
  ) {}
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    request = request.clone({
      withCredentials: true,
      setHeaders: {
        "Cache-Control": "no-cache, no-store, must-revalidate",
        "Pragma": "no-cache",
        "Expires": "0",
        "X-XSRF-TOKEN": <string>this.xsrf.getToken() || (Math.random() + 1).toString(36).substring(2),
      },
    });
    return next.handle(request).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          this.notification.error(
            err.status.toString(),
            err.error != null ? err.error.title || err.error : err.message,
            { nzDuration: 0 }
          );
        }
        return throwError(() => err);
      })
    );
  }
}
