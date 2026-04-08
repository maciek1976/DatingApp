import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private accountService = inject(AccountService);
  
  constructor() {
    // Initialization logic can be added here if needed
  }

  init() {
    const user = localStorage.getItem('user');
    if (!user) return of(null); // No user found, initialization complete
    this.accountService.currentUser.set(JSON.parse(user));

    return of(null); // Return an observable to indicate initialization is complete
  }
}
