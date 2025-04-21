import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Emprunt } from '../models/emprunt.model';

@Injectable({
  providedIn: 'root'
})
export class EmpruntService {
  private apiUrl = 'https://localhost:5039/api/Emprunts';

  constructor(private http: HttpClient) { }

  getEmprunts(): Observable<Emprunt[]> {
    return this.http.get<Emprunt[]>(this.apiUrl);
  }

  getEmprunt(id: number): Observable<Emprunt> {
    return this.http.get<Emprunt>(`${this.apiUrl}/${id}`);
  }

  getEmpruntsByUtilisateur(utilisateurId: number): Observable<Emprunt[]> {
    return this.http.get<Emprunt[]>(`${this.apiUrl}/utilisateur/${utilisateurId}`);
  }

  ajouterEmprunt(emprunt: Emprunt): Observable<Emprunt> {
    return this.http.post<Emprunt>(this.apiUrl, emprunt);
  }

  retournerEmprunt(id: number): Observable<Emprunt> {
    return this.http.put<Emprunt>(`${this.apiUrl}/retour/${id}`, {});
  }

  supprimerEmprunt(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
