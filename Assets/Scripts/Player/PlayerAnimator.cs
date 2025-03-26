using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles interactions with the animator component of the player
/// It reads the player's state from the controller and animates accordingly
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The player controller script to read state information from")]
    public PlayerController playerController;
    [Tooltip("The animator component that controls the player's animations")]
    public Animator animator;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first update
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Start()
    {
        ReadPlayerStateAndAnimate();
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called every frame
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Update()
    {
        ReadPlayerStateAndAnimate();
    }

    /// <summary>
    /// Description:
    /// Reads the player's state and then sets and unsets booleans in the animator accordingly
    /// Input:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    void ReadPlayerStateAndAnimate()
{
    // Reset all animation flags first
    animator.SetBool("isIdle", false);
    animator.SetBool("isJumping", false);
    animator.SetBool("isFalling", false);
    animator.SetBool("isRunning", false);
    animator.SetBool("isDead", false);
    animator.SetBool("isRespawning", false); // Add this line if it's not already there

    // Only play one animation based on current state
    switch (playerController.state)
    {
        case PlayerController.PlayerState.Idle:
            animator.SetBool("isIdle", true);
            break;
        case PlayerController.PlayerState.Jump:
            animator.SetBool("isJumping", true);
            break;
        case PlayerController.PlayerState.Fall:
            animator.SetBool("isFalling", true);
            break;
        case PlayerController.PlayerState.Walk:
            animator.SetBool("isRunning", true);
            break;
        case PlayerController.PlayerState.Dead:
            animator.SetBool("isDead", true);
            break;
        case PlayerController.PlayerState.Respawning:
            animator.SetBool("isRespawning", true); // ✅ Only animate Respawn here!
            break;
    }
}
}
