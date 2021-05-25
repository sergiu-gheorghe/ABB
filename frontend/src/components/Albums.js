import {Table} from 'react-bootstrap'
import React, {useState, useEffect} from 'react'
import Paging from './Paging'

export default function Albums() {
    const [currentPage, setCurrentPage] = useState(1);
    const[paginatedList, setData] = useState({
        items: [],
        totalCount: 0,
        pageIndex: 1
     });

     const [albumId, setAlbumId] = useState(null);
     const [title, setTitle] = useState('');
     const [userId, setUserId] = useState(null);

    useEffect(() => {
        let url = `https://localhost:55002/api/v1/albums?pageNumber=${currentPage}`;
        if(albumId){
            url +=`&id=${albumId}`;
        } 
        if(title){
            url +=`&title=${title}`;
        }
        if(userId){
            url +=`&userId=${userId}`;
        }

        fetch(url)
        .then((response) => response.json())
        .then((json) => setData(json))
        .catch(err => console.log(err.message));
    }, [currentPage, albumId, title, userId]);

    const paginate = pageNumber => setCurrentPage(pageNumber);

    const handleAlbumIdChange = e => { 
        setAlbumId(e.target.value);
        setCurrentPage(1);
    };

    const handleTitleChange = e => { 
        setTitle(e.target.value);
        setCurrentPage(1); 
    };

    const handleUserIdChange = e => {
        setUserId(e.target.value);
        setCurrentPage(1);
    };

    return (
        <React.Fragment>
        <div className="container">
            <br/>
            <span>Album Id:</span><input type="text" value={albumId} onChange={handleAlbumIdChange}/>
            <span>Title:</span><input type="text" value={title} onChange={handleTitleChange}/>
            <span>User Id:</span><input type="text" value={userId} onChange={handleUserIdChange}/>
        </div>
        <Table className="mt-4" striped bordered hover size="sm">
            <thead>
                <th>Album Id</th>
                <th>Title</th>
                <th>User Id</th>
            </thead>
            <tbody>
                {
                    paginatedList.items.map(i => 
                        <tr key={i.id}>
                            <td>{i.id}</td>
                            <td>{i.title}</td>
                            <td>{i.userId}</td>
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