using Microsoft.EntityFrameworkCore;
using Wireframe.Backend.Contexts;
using Wireframe.Backend.Models;

namespace Wireframe.Backend.Repositories
{
    public class WireFrameRepository : IWireFrameRepository
    {
        public async  Task<WireframeModel> GetById(string id)
        {
            using var context = new WireframeContext();

            try
            {

            var taskValue = context.WireframeModel
                .Include(e => e.Devices)
                .FirstAsync(e => e.Id.Equals(id));

               WireframeModel? wireframeModel = await taskValue ?? throw new ArgumentException($"Not Found Wireframe with id:  {id} ");

                return wireframeModel;
            }catch( Exception ex) when( ex is InvalidOperationException || ex is ArgumentNullException || ex is OperationCanceledException) 
            {
                throw new ArgumentException($"Provided Wireframe id contains token no formal Guid Id: { ex.Message}");
            }
            
        }

       

        public async Task<WireframeModel> Post(WireframeModel wireframeModel)
        {
            using var context = new WireframeContext();
            
                var taskValue = context.WireframeModel.
                    FindAsync(wireframeModel.Id);
                

                WireframeModel? wireframe = await taskValue;

                if(wireframe !=null) throw  new ArgumentException($"Entity is already recorded===> Found WireframeModel with id:  {wireframeModel.Id} ");

                try
                {
                 wireframeModel.Id= Guid.NewGuid().ToString();
                
                foreach(var d in wireframeModel.Devices)
                            d.Id = Guid.NewGuid().ToString();
                                  
                    
                    await context.AddAsync(wireframeModel).ConfigureAwait(false);

                    await context.SaveChangesAsync();

                    return wireframeModel;

                }
            catch (Exception ex) when (ex is ArgumentNullException || ex is NotSupportedException || ex is DbUpdateException || ex is DbUpdateConcurrencyException
                        || ex is OperationCanceledException)
            {
                throw new Exception($" Problem Server site provided body payload {ex.Message}");
            }


        }

        public async  Task<Device> Put(string id, Device device)
        {
            try
            {

            using var context = new WireframeContext();
            

                var taskValue = context.WireframeModel.
                   FindAsync(id);


            WireframeModel? wireframeModel = await taskValue;
            
            if(wireframeModel == null)
                throw new ArgumentException($"Not Found Wireframe with id:  {id} ");

            Device? target = wireframeModel.Devices.FirstOrDefault((Device d) => d.Id != null && d.Id.Equals(device.Id));
            
            if(target != null)
                throw new ArgumentException($"Entity is already recorded===> Found Todo with: {device} ");

               device.Id = Guid.NewGuid().ToString();

                context.Add(device);

                wireframeModel.Devices.Add(device);

                context.Update(wireframeModel);

                context.SaveChanges();
                return device;
            }catch( Exception ex) when( ex is ArgumentNullException || ex is  NotSupportedException  || ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                throw new Exception($" Problem Server site either provided parameters id or body payload {ex.Message}");
            }
            
        }

        public async Task<Device> ModifyDevice(string id, Device device)
        {
            try
            {

                using var context = new WireframeContext() ;
            
                var taskValue = context.WireframeModel.FindAsync(id);


                WireframeModel? wireframeModel = await taskValue ?? throw new ArgumentException($"Not Found Wireframe with id:  {id} ");

                Device? target = wireframeModel.Devices.FirstOrDefault((Device d) => d.Id != null && d.Id.Equals(device.Id));

                if(target != null) throw new ArgumentException($"Entity is already recorded===> Found Todo with: {device} ");

                wireframeModel.Devices.Remove(target!);

                wireframeModel.Devices.Add(device);

                context.Update(wireframeModel);

                context.SaveChanges();

                return device;
            }catch(Exception ex) when(ex is ArgumentNullException || ex is NotSupportedException || ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {

                throw new Exception($" Problem Server site either provided parameters id or body payload {ex.Message}");
            }
            
        }

        public async Task<IEnumerable<WireframeModel>> GetAll()
        {
            try
            {

            using var context = new WireframeContext();

            var targets = await context.WireframeModel.Include(w => w.Devices).
                ToListAsync() ?? throw new ArgumentException($"NOT FOUND LIST");

            return targets;
            }catch( Exception ex ) when( ex is ArgumentNullException ||  ex is OperationCanceledException)
            {
                throw new Exception($" Problem Server site {ex.Message}");
            }
        }

        public async Task<Device> Delete(string parentId, string id)
        {
            try
            {
                using var context = new WireframeContext();
                var taskValue = context.WireframeModel.Include(e => e.Devices).FirstOrDefaultAsync( e=> e.Id.Equals(parentId) );


                WireframeModel? wireframeModel = await taskValue ?? throw new ArgumentException($"Not Found Wireframe with id:  {parentId} ");

                Device target = wireframeModel.Devices.FirstOrDefault((Device d) => d.Id != null && d.Id.Equals(id)) ?? throw new ArgumentException($"Not Found Wireframe with id:  {id} ");

                wireframeModel.Devices.Remove(target);

                wireframeModel.Devices.Remove(target);

                context.SaveChanges();


                //context.Update(wireframeModel);

               // context.SaveChanges();

                return target;
            }
            catch( Exception ex) when( ex is ArgumentNullException)
            {
                throw new Exception($"Either ParentId: ${parentId} was null or id $:{id} was null or NOT FOUND width combining  ParentId {parentId} and id: {id} ");
            }
        }
    }
}
