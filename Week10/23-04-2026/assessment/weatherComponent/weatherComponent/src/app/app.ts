import { Component } from '@angular/core';
import { WeatherComponent } from './weather/weather';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [WeatherComponent],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  weatherData = [
    // Americas
    { name: 'New York',     temperature: '22°C', wind: '15 km/h', humidity: '56%' },
    { name: 'Los Angeles',  temperature: '28°C', wind: '8 km/h',  humidity: '45%' },
    { name: 'Toronto',      temperature: '15°C', wind: '22 km/h', humidity: '62%' },
    { name: 'Chicago',      temperature: '17°C', wind: '30 km/h', humidity: '58%' },
    { name: 'São Paulo',    temperature: '26°C', wind: '11 km/h', humidity: '78%' },

    // Europe
    { name: 'London',       temperature: '18°C', wind: '20 km/h', humidity: '70%' },
    { name: 'Paris',        temperature: '20°C', wind: '12 km/h', humidity: '60%' },
    { name: 'Berlin',       temperature: '16°C', wind: '18 km/h', humidity: '65%' },
    { name: 'Madrid',       temperature: '30°C', wind: '9 km/h',  humidity: '38%' },
    { name: 'Rome',         temperature: '27°C', wind: '7 km/h',  humidity: '52%' },

    // Asia
    { name: 'Tokyo',        temperature: '28°C', wind: '10 km/h', humidity: '65%' },
    { name: 'Mumbai',       temperature: '34°C', wind: '14 km/h', humidity: '82%' },
    { name: 'Dubai',        temperature: '41°C', wind: '16 km/h', humidity: '30%' },
    { name: 'Singapore',    temperature: '31°C', wind: '13 km/h', humidity: '84%' },
    { name: 'Beijing',      temperature: '24°C', wind: '19 km/h', humidity: '48%' },

    // Africa & Oceania
    { name: 'Sydney',       temperature: '25°C', wind: '18 km/h', humidity: '50%' },
    { name: 'Melbourne',    temperature: '19°C', wind: '23 km/h', humidity: '55%' },
    { name: 'Cairo',        temperature: '38°C', wind: '11 km/h', humidity: '25%' },
    { name: 'Cape Town',    temperature: '21°C', wind: '26 km/h', humidity: '67%' },
    { name: 'Nairobi',      temperature: '23°C', wind: '10 km/h', humidity: '71%' },
  ];
}