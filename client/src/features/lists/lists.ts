import { Component, inject } from '@angular/core';
import { MemberService } from '../../core/services/member-service';
import { Observable } from 'rxjs';
import { Member } from '../../types/member';
import { AsyncPipe } from '@angular/common';
import { MemberCard } from "../members/member-card/member-card";

@Component({
  selector: 'app-lists',
  imports: [AsyncPipe, MemberCard],
  templateUrl: './lists.html',
  styleUrl: './lists.css',
})
export class Lists {
  private memberService = inject(MemberService);
  protected members$: Observable<Member[]>; 

  constructor() {
    this.members$ = this.memberService.getMembers();
  }
}
