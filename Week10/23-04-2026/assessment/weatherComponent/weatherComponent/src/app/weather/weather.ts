import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface WeatherRecord {
  name: string;
  temperature: string;
  wind: string;
  humidity: string;
}

@Component({
  selector: 'app-weather',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './weather.html',
  styleUrls: ['./weather.css']
})
export class WeatherComponent {
  @Input() weatherData: WeatherRecord[] = [];

  searchQuery: string = '';

  get filteredResult(): WeatherRecord | null {
    if (!this.searchQuery.trim()) return null;
    const match = this.weatherData.find(
      city => city.name.toLowerCase() === this.searchQuery.trim().toLowerCase()
    );
    return match || null;
  }

  get showNoResults(): boolean {
    return this.searchQuery.trim().length > 0 && this.filteredResult === null;
  }
}