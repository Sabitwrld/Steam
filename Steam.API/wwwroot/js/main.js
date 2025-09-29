const apiBase = 'https://localhost:7257/api';

async function loadFilters() {
    const [genresRes, tagsRes] = await Promise.all([
        fetch(`${apiBase}/genre`),
        fetch(`${apiBase}/tag`)
    ]);

    const genres = await genresRes.json();
    const tags = await tagsRes.json();

    const genreSelect = document.getElementById('genreFilter');
    genres.forEach(g => genreSelect.appendChild(new Option(g.name, g.id)));

    const tagSelect = document.getElementById('tagFilter');
    tags.forEach(t => tagSelect.appendChild(new Option(t.name, t.id)));
}

async function loadCatalog() {
    const genreId = document.getElementById('genreFilter').value;
    const tagId = document.getElementById('tagFilter').value;
    let url = `${apiBase}/applicationcatalog?page=1&pageSize=20`;
    if (genreId) url += `&genreId=${genreId}`;
    if (tagId) url += `&tagId=${tagId}`;

    const res = await fetch(url);
    const data = await res.json();

    const container = document.getElementById('catalogContainer');
    container.innerHTML = '';

    data.data.forEach(app => {
        const card = document.createElement('div');
        card.className = 'app-card';
        card.innerHTML = `<h3>${app.name}</h3><p>${app.description.substring(0,50)}...</p>`;
        card.addEventListener('click', () => openModal(app.id));
        container.appendChild(card);
    });
}

async function openModal(appId) {
    const res = await fetch(`${apiBase}/applicationcatalog/${appId}`);
    const app = await res.json();

    document.getElementById('modalName').textContent = app.name;
    document.getElementById('modalDescription').textContent = app.description;

    const mediaRes = await fetch(`${apiBase}/media/application/${appId}`);
    const mediaList = await mediaRes.json();
    const mediaContainer = document.getElementById('modalMedia');
    mediaContainer.innerHTML = mediaList.map(m => `<img src="${m.url}" class="media-img">`).join('');

    const sysRes = await fetch(`${apiBase}/systemrequirements/application/${appId}`);
    const sysList = await sysRes.json();
    const tbody = document.getElementById('modalRequirements').querySelector('tbody');
    tbody.innerHTML = '';
    sysList.forEach(r => {
        const tr = document.createElement('tr');
        tr.innerHTML = `<td>${r.requirementType}</td><td>${r.os}</td><td>${r.cpu}</td><td>${r.gpu}</td><td>${r.ram}</td><td>${r.storage}</td><td>${r.additionalNotes}</td>`;
        tbody.appendChild(tr);
    });

    document.getElementById('appModal').style.display = 'flex';
}

document.getElementById('modalClose').addEventListener('click', () => {
    document.getElementById('appModal').style.display = 'none';
});

document.getElementById('genreFilter').addEventListener('change', loadCatalog);
document.getElementById('tagFilter').addEventListener('change', loadCatalog);

loadFilters().then(loadCatalog);
