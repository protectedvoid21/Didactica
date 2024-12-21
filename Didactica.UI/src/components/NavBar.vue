<template>
  <header>
    <nav class='flex justify-between'>
      <div class='text-6xl'>{{ new Date().toLocaleDateString('pl-PL', dateFormatOptions) }}</div>
    </nav>
  </header>
</template>


<script setup lang="ts">

const dateFormatOptions: Intl.DateTimeFormatOptions = {
  year: 'numeric',
  month: 'long',
  day: 'numeric',
};
</script>