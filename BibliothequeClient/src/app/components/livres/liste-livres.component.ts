import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Livre } from '../../models/livre.model';
import { LivreService } from '../../services/livre.service';

@Component({
  selector: 'app-liste-livres',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './liste-livres.component.html',
})
export class ListeLivresComponent implements OnInit {
  livres: Livre[] = [];
  livreActuel: Livre = this.createEmptyLivre();
  modeEdition: boolean = false;

  constructor(private livreService: LivreService) {}

  ngOnInit(): void {
    this.fetchLivres();
  }

  // Fonction pour initialiser un objet Livre vide
  createEmptyLivre(): Livre {
    return {
      titre: '',
      auteur: '',
      isbn: '',
      categorie: '',
      annee: 0,
      description: '',
      nombreExemplaires: 0,
      exemplairesDisponibles: 0
    };
  }

  // Fonction pour récupérer les livres à partir du service
  fetchLivres(): void {
    this.livreService.getLivres().subscribe({
      next: (livres) => this.livres = livres,
      error: (error) => console.error('Erreur lors du chargement des livres :', error),
    });
  }

  // Fonction pour sélectionner un livre et activer le mode édition
  selectLivre(livre: Livre): void {
    this.modeEdition = true;
    this.livreActuel = { ...livre }; // Clonage pour éviter la modification directe
  }

  // Fonction pour ajouter un nouveau livre
  addLivre(): void {
    this.livreService.ajouterLivre(this.livreActuel).subscribe({
      next: (nouveauLivre) => {
        this.livres.push(nouveauLivre);
        this.resetEditionMode();
      },
      error: (error) => console.error('Erreur lors de l\'ajout du livre :', error),
    });
  }

  // Fonction pour mettre à jour les informations d'un livre
  updateLivre(): void {
    if (!this.livreActuel.id) return;

    this.livreService.modifierLivre(this.livreActuel).subscribe({
      next: () => {
        const index = this.livres.findIndex((livre) => livre.id === this.livreActuel.id);
        if (index !== -1) {
          this.livres[index] = { ...this.livreActuel };
        }
        this.resetEditionMode();
      },
      error: (error) => console.error('Erreur lors de la mise à jour du livre :', error),
    });
  }

  // Fonction pour supprimer un livre
  deleteLivre(id: number): void {
    this.livreService.supprimerLivre(id).subscribe({
      next: () => {
        this.livres = this.livres.filter((livre) => livre.id !== id);
        this.resetEditionMode();
      },
      error: (error) => console.error('Erreur lors de la suppression du livre :', error),
    });
  }

  // Fonction pour réinitialiser le mode d'édition
  resetEditionMode(): void {
    this.modeEdition = false;
    this.livreActuel = this.createEmptyLivre();
  }
}
