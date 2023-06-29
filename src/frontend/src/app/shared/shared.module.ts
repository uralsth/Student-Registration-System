import { NgModule } from '@angular/core';
import {MatTableModule} from '@angular/material/table'; 
import { HttpClientModule} from '@angular/common/http'


@NgModule({
  declarations: [],
  imports: [
    MatTableModule,
    HttpClientModule
    
  ],
  exports: [
    MatTableModule,
    HttpClientModule

  ]
})
export class SharedModule { }
