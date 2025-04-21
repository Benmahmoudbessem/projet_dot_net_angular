import { Routes } from '@angular/router';
import { ListeLivresComponent } from './components/livres/liste-livres.component';
import { ListeUtilisateursComponent } from './components/utilisateurs/liste-utilisateurs.component';
import { ListeEmpruntsComponent } from './components/emprunts/liste-emprunts.component';

export const routes: Routes = [
  { path: '', redirectTo: '/livres', pathMatch: 'full' },
  { path: 'livres', component: ListeLivresComponent },
  { path: 'utilisateurs', component: ListeUtilisateursComponent },
  { path: 'emprunts', component: ListeEmpruntsComponent },
  { path: '**', redirectTo: '/livres' }
];
