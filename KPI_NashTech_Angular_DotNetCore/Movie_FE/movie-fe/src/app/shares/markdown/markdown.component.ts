import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-markdown',
  templateUrl: './markdown.component.html',
  styleUrls: ['./markdown.component.css']
})
export class MarkdownComponent implements OnInit {

  constructor() { }

  @Output()
  changeMarkdown = new EventEmitter<string>();

  @Input()
  markdownContent: string|any;

  ngOnInit(): void {
  }

}
