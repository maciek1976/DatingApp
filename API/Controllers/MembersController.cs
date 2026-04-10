using Api.Interfaces;
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
}
