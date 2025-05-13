using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearObstacle : MonoBehaviour
{
    public GameObject bearPrefab; 
    public Transform[] spawnPaths; 
    public Transform[] destinationPoints; 
    public float minSpawnDelay = 2f; 
    public float maxSpawnDelay = 5f; 
    public float moveSpeed = 5f;
    public float knockbackForce = 20f;
    
    private Coroutine spawnCoroutine;

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnBearRoutine());
        }
    }
    
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
    
    IEnumerator SpawnBearRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnBear();
        }
    }

    // 곰 생성
    void SpawnBear()
    {
        int randomIndex = Random.Range(0, spawnPaths.Length);
        Transform spawnPoint = spawnPaths[randomIndex];
        Transform destination = destinationPoints[randomIndex];
        
        GameObject bear = Instantiate(bearPrefab, spawnPoint.position, spawnPoint.rotation);
        bear.transform.rotation = Quaternion.LookRotation(destination.position - spawnPoint.position);
        bear.AddComponent<Bear>().Init(destination.position, moveSpeed, knockbackForce);
    }
    
    //이동 + 충돌 처리
    private class Bear : MonoBehaviour
    {
        private Vector3 target;
        private float speed;
        private float lifetime;
        private float knockback;
        private Animator animator;
        private bool isAttacking = false;
        private bool hasHitPlayer = false;
        
        public void Init(Vector3 dest, float moveSpeed, float knockbackForce)
        {
            target = dest;
            speed = moveSpeed;
            knockback = knockbackForce;
            
            animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("isRunning", true); 
            }
        }

        private void Update()
        {
            if (isAttacking) return;
            
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision  other)
        {
            if (hasHitPlayer) return;
            
            if (other.gameObject.CompareTag("Player"))
            {
                hasHitPlayer = true;
                isAttacking = true; 
                
                Rigidbody rb = other.rigidbody;
                if (rb != null)
                {
                    Vector3 dir = (other.transform.position - transform.position);
                    dir.y = 0f;
                    dir.Normalize();

                    StartCoroutine(TemporaryKnockback(rb, dir * knockback));
                }
                
                //로프 없애기
                RopeAction rope = other.gameObject.GetComponentInChildren<RopeAction>();
                if (rope != null && rope.IsGrappling)
                {
                    rope.SendMessage("DeleteRope");
                    rope.StartCoroutine(RopeCooldown(rope, 1f)); // 1초 동안 비활성화
                }

                IEnumerator RopeCooldown(RopeAction rope, float delay)
                {
                    rope.enabled = false;
                    yield return new WaitForSeconds(delay);
                    rope.enabled = true;
                }
                
                IEnumerator TemporaryKnockback(Rigidbody rb, Vector3 knockbackVelocity)
                {
                    float duration = 0.5f; // 밀리는 시간
                    float timer = 0f;

                    while (timer < duration)
                    {
                        rb.velocity = knockbackVelocity;
                        timer += Time.deltaTime;
                        yield return null;
                    }
                }


                if (animator != null)
                {
                    animator.SetBool("Run", false);
                    animator.SetTrigger("Attack"); 
                }
                
                StartCoroutine(ResumeRunAfterAttack());
            }
        }
        
        private IEnumerator ResumeRunAfterAttack()
        {
            yield return new WaitForSeconds(1.0f);
            isAttacking = false;
            if (animator != null)
            {
                animator.SetBool("Run", true);
            }
        }
    }
}
    



