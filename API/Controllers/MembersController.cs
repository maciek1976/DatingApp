using System.Security.Claims;
using Api.Interfaces;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//https://localhost:5001/api/members
[Authorize]
public class MembersController(IMemberRepository memberRepository) : BaseApiController
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
    {
        return Ok(await _memberRepository.GetMembersAsync());
    }

    [HttpGet("{id}")] //https://localhost:5001/api/members/bob-idS
    public async Task<ActionResult<Member>> GetMember(string id)
    {
        var member = await _memberRepository.GetMemberByIdAsync(id);

        return member == null ? NotFound() : member;
    }

    [HttpGet("{id}/photos")] //https://localhost:5001/api/members/bob-idS/photos
    public async Task<ActionResult<IReadOnlyList<Photo>>> GetMemberPhotos(string id)
    {
        return Ok(await _memberRepository.GetPhotosForMemberAsync(id));
    }

    [HttpPut] //https://localhost:5001/api/members/
    public async Task<ActionResult> UpdateMember(MemberUpdateDto memberUpdateDto)
    {
        var memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (memberId == null) return BadRequest("Oops - no id found in token");

        var member = await _memberRepository.GetMemberByIdAsync(memberId);
        if (member == null) return BadRequest("Member not found");
        
        member.DisplayName = memberUpdateDto.DisplayName ?? member.DisplayName;
        member.Description = memberUpdateDto.Description ?? member.Description;
        member.City = memberUpdateDto.City ?? member.City;
        member.Country = memberUpdateDto.Country ?? member.Country;

        member.User.DisplayName = memberUpdateDto.DisplayName ?? member.User.DisplayName;

        _memberRepository.Update(member);

        if (await _memberRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update member");
    }

}
