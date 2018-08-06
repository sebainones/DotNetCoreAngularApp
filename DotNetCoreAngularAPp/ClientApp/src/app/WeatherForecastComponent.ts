import { Component, Input } from '@angular/core';

import { WeatherForeCast } from './WeatherForeCast';

@Component({
  selector: 'WeatherForeCast',
  template: `
    <div class="WeatherForeCast">
      <p>{{ weatherForeCast.name }}</p>
      <p>{{ weatherForeCast.summary }}</p>
    </div>
  `,
})
export class WeatherForeCastComponent {
  @Input() weatherForeCast: WeatherForeCast;
}
