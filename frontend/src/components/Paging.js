import Pagination from 'react-bootstrap/Pagination'

export default function Paging({totalPages, activePage, onChange}) {
    let items = [];
    for (let number = 1; number <= totalPages; number++) {
        items.push(
            <Pagination.Item 
                key={number} 
                active={number === activePage} 
                onClick={() => onChange(number)}>
                {number}
            </Pagination.Item>);
    }

    return (
        <div>
            <Pagination>{items}</Pagination>
        </div>)
}
