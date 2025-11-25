import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const allowedRoles = route.data['roles'].map((r: string) =>
      this.auth.normalizeRole(r)
    );

    const userRole = this.auth.getRole();

    if (!allowedRoles.includes(userRole)) {
      this.router.navigate(['/not-authorized']);
      return false;
    }

    return true;
  }
}
