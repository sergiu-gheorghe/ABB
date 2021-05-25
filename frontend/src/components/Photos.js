import {Table} from 'react-bootstrap'
import React, {useState, useEffect} from 'react'
import Paging from './Paging'

export default function Photos() {
    const [currentPage, setCurrentPage] = useState(1);
    const[paginatedList, setData] = useState({
        items: [],
        totalCount: 0,
        pageIndex: 1
     });

    const paginate = pageNumber => setCurrentPage(pageNumber);

    const [albumId, setAlbumId] = useState(null);
    const [title, setTitle] = useState('');

    useEffect(() => {
        let url = `https://localhost:55002/api/v1/photos?pageNumber=${currentPage}&pageSize=200`;
        if(albumId){
            url +=`&albumId=${albumId}`;
        } 
        if(title){
            url +=`&title=${title}`;
        }

        fetch(url)
        .then((response) => response.json())
        .then((json) => setData(json))
        .catch(err => console.log(err.message));
    }, [currentPage, albumId, title]);

    const handleAlbumIdChange = e => { 
        setAlbumId(e.target.value);
        setCurrentPage(1);
    };

    const handleTitleChange = e => { 
        setTitle(e.target.value);
        setCurrentPage(1); 
    };

    return (
        <React.Fragment>
        <div className="container">
            <br/>
            <span>Album Id:</span><input type="text" value={albumId} onChange={handleAlbumIdChange}/>
            <span>Title:</span><input type="text" value={title} onChange={handleTitleChange}/>
        </div>
        <Table className="mt-4" striped bordered hover size="sm">
            <thead>
                <th>Id</th>
                <th>Title</th>
                <th>Album Id</th>
                <th>Url</th>
            </thead>
            <tbody>
                {
                    paginatedList.items.map(p => 
                        <tr key={p.id}>
                            <td>{p.id}</td>
                            <td>{p.title}</td>
                            <td>{p.albumId}</td>
                            <td>
                                <img 
                                    src={p.thumbnailUrl} 
                                    alt={p.title} 
                                    onMouseOver={e => (e.currentTarget.src = p.url)}
                                    onMouseOut={e => (e.currentTarget.src = p.thumbnailUrl)}/></td>
                        </tr>
                        )
                }
            </tbody>
        </Table>

        <div className="mt-3">
        <Paging
            totalPages={paginatedList.totalPages}
            activePage = {paginatedList.pageIndex}
            onChange={paginate}
        />
        </div>
        </React.Fragment>
    )
}